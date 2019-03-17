<template>
  <el-dialog @closed="$emit('close')" width="60%" :visible.sync="isOpen" append-to-body>
    <div slot="title">
      <p>{{ticket.ticketReason}}</p>
      <span class="edit">{{ticket.userFullName}} - {{ticket.userUserName}}</span>
      <el-button
        v-show="ticket.state===0"
        class="float-right"
        style=" margin-left: 30px;"
        @click="changeTicketState(1)"
        type="warning"
        plain
      >بستن تیکت</el-button>

      <el-button
        class="float-right"
        style=" margin-left: 30px;"
        @click="isChangeCategoryDialogOpen=ticket"
        type="warning"
        plain
      >تغییر بخش</el-button>

      <el-button
        v-show="ticket.state===1"
        class="float-right"
        style=" margin-left: 30px;"
        @click="changeTicketState(0)"
        type="primary"
        plain
      >باز کردن تیکت</el-button>
    </div>
    <div class="chat-history" ref="chathistory">
      <ul>
        <template v-for="item in ticket.ticketItems">
          <li v-if="item.userId!==ticket.userId" class="clearfix">
            <div class="message-data align-right">
              <span class="message-data-name">{{item.userFullName}}</span>
              <i class="fa fa-circle me"></i>
              <span class="message-data-time">{{ item.createdOn | persian-date }}</span> &nbsp; &nbsp;
            </div>
            <div :class="['message','other-message','float-left',(editing===item?'editing':'')]">
              <p>{{item.message}}</p>
              <span class="edit" v-show="item.isEdited">ویرایش شده</span>
              <span v-show="editing!==item" class="edit float-right" @click="setEditing(item)">
                ویرایش
                <i class="el-icon-edit"></i>
              </span>
              <span v-show="editing===item" class="edit float-right">
                درحال ویرایش ...
                <i class="el-icon-edit"></i>
              </span>
            </div>
          </li>

          <li class="clearfix" v-if="item.userId===ticket.userId">
            <div class="message-data align-left">
              <span class="message-data-name">
                <i class="fa fa-circle online"></i>
                {{item.userFullName}}
              </span>
              <span class="message-data-time">{{ item.createdOn | persian-date }}</span>
            </div>
            <div class="message my-message float-right">{{item.message}}</div>
          </li>
        </template>
      </ul>
    </div>
    <el-alert v-show="editing" title="در حال ویرایش" type="warning" @close="cancelEdit"></el-alert>
    <el-form :inline="true" class="form" ref="form" label-width="120px">
      <el-form-item>
        <el-button type="primary" @click.prevent="submit">
          <i class="fas fa-paper-plane"></i>
          ارسال
        </el-button>
      </el-form-item>
      <textarea
        ref="textarea"
        v-model="message"
        @keyup.enter.exact="submit"
        placeholder="متن پیام"
        class="message-box"
        rows="5"
      ></textarea>
    </el-form>
    <change-category-dialog 
      @onCategoryChange="$emit('onCategoryChange')"
      :item="ticket"
      :isOpen="isChangeCategoryDialogOpen"
      @close="isChangeCategoryDialogOpen=undefined"
    ></change-category-dialog>
  </el-dialog>
</template>
<script>
import axios from "axios";
import ChangeCategoryDialog from "./change-category-dialog";
export default {
  name: "app",
  props: {
    isOpen: {
      type: Boolean
    },
    ticket: {
      type: Object
    }
  },
  components: {
    ChangeCategoryDialog
  },
  data() {
    return {
      message: "",
      isChangeCategoryDialogOpen: false,
      editing: undefined
    };
  },
  methods: {
    changeTicketState(state) {
      this.$confirm("از تغییر وضعیت این تیکت اطمینان دارید؟", "هشدار", {
        confirmButtonText: "بلی",
        cancelButtonText: "خیر",
        type: "warning"
      }).then(() => {
        axios
          .patch("/api/tickets/changestate", {
            ticketId: this.ticket.id,
            state
          })
          .then(res => {
            this.ticket.state = state;
            this.$emit("ticketstatechange", state);
            this.$message({
              type: "success",
              message: "انجام شد"
            });
          });
      });
    },
    cancelEdit() {
      this.editing = null;
      this.message = "";
    },
    setEditing(editing) {
      this.message = editing.message;
      this.editing = editing;
      this.$refs.textarea.focus();
    },
    scrollToBottom() {
      setTimeout(() => {
        this.$refs.chathistory.scrollTop = this.$refs.chathistory.scrollHeight;
      }, 100);
    },
    submit() {
      if (!this.message.trim()) return;
      if (this.editing) {
        this.updateEditing();
        return;
      }
      this.addNewAnswer();
    },
    updateEditing() {
      axios
        .patch("/api/tickets/ticketitems", {
          ticketItemId: this.editing.id,
          message: this.message
        })
        .then(res => {
          this.editing.isEdited = true;
          this.editing.message = this.message;
          this.editing = undefined;
          this.message = "";
        });
    },
    addNewAnswer() {
      axios
        .post("/api/tickets/ticketitems", {
          ticketId: this.ticket.id,
          message: this.message
        })
        .then(res => {
          this.ticket.ticketItems = [...this.ticket.ticketItems, res.data.data];
          this.scrollToBottom();
          this.message = "";
        });
    }
  },
  mounted() {
    this.scrollToBottom();
  },
  watch: {
    message() {
      let match = /\r|\n/.exec(this.message);
      if (match) {
        this.message = this.message.trim();
      }
    },
    "ticket.ticketItems.length"() {
      this.scrollToBottom();
    },
    ticket() {
      this.scrollToBottom();
    }
  }
};
</script>

<style scoped>
.edit {
  font-size: 12px;
  cursor: pointer;
}

.form {
  padding-top: 10px;
}

.form textarea {
  width: 80%;
  border: none;
  overflow: auto;
  outline: none;
  -webkit-box-shadow: none;
  -moz-box-shadow: none;
  box-shadow: none;
  resize: none;
}

ul {
  list-style: none;
}

.chat-history {
  padding: 30px 30px 20px;
  overflow-y: scroll;
  height: 505px;
  border-bottom: 1px solid #eaeaea;
  border-top: 1px solid #eaeaea;
}

.chat-history .message-data {
  margin-bottom: 15px;
}

.chat-history .message-data-time {
  color: #a8aab1;
  padding-left: 6px;
}

.chat-history .editing {
  border: 2px solid #17a2b8;
}

.chat-history .message {
  color: #696969;
  padding: 18px 20px;
  line-height: 26px;
  font-size: 16px;
  border-radius: 7px;
  margin-bottom: 30px;
  width: 90%;
  position: relative;
}

.chat-history .message:after {
  bottom: 100%;
  left: 7%;
  border: solid transparent;
  content: " ";
  height: 0;
  width: 0;
  position: absolute;
  pointer-events: none;
  border-bottom-color: #e5fddb;
  border-width: 10px;
  margin-left: -10px;
}

.chat-history .my-message {
  background: #e5fddb;
}

.chat-history .other-message {
  background: #e3f0ff;
}

.chat-history .other-message:after {
  border-bottom-color: #e3f0ff;
  left: 93%;
}

.online,
.me {
  margin-right: 3px;
  font-size: 10px;
}

.online {
  color: #e5fddb;
}

.me {
  color: #e3f0ff;
}

.align-left {
  text-align: left;
}

.align-right {
  text-align: right;
}

.float-right {
  float: right;
}

.clearfix:after {
  visibility: hidden;
  display: block;
  font-size: 0;
  content: " ";
  clear: both;
  height: 0;
}
</style>
