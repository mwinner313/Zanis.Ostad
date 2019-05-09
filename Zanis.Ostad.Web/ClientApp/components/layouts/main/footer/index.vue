<template>
  <footer>
    <div class="container">
      <div class="row">
        <div class="col-sm-6">
          <div class="social-links">
            <ul>
              <li>
                <a href="#"> <i class="fab fa-instagram"></i> </a>
              </li>
              <li>
                <a href="#"><i class="fab fa-facebook-f"></i></a>
              </li>
              <li>
                <a href="#"><i class="fab fa-google-plus-g"></i></a>
              </li>
              <li>
                <a href="#"><i class="fab fa-twitter"></i></a>
              </li>
            </ul>
          </div>

          <h5 class="text-center text-white copyright">
            تمام حقوق متعلق به استاد زانیس می باشد.
          </h5>

        </div>
        <div class="col-sm-6 other-links ">
          <div class="container">
            <div class="row">
              <div class="col-sm-6">
                <span class="text-white">لینک های سریع پیام نور</span>
                <ul>
                  <li><a href="#">آيين نامه هاي دانشجويي</a></li>
                  <li><a href="#">آيين نامه هاي دانشجويي</a></li>
                  <li><a href="#">آيين نامه هاي دانشجويي</a></li>
                </ul>
              </div>
              <div class="col-sm-6 mt-2">
                <span class="text-white">لینک های سریع پیام نور</span>
                <ul>
                  <li><a href="#">آيين نامه هاي دانشجويي</a></li>
                  <li><a href="#">آيين نامه هاي دانشجويي</a></li>
                  <li><a href="#">آيين نامه هاي دانشجويي</a></li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

  </footer>
</template>

<script>
    export default {
        name: ""
    }
</script>

<style scoped lang="scss">
@import '../../../../assets/_variables.scss';
footer {
  background-color: #414040;
  padding-top: 110px;
  .social-links {
    margin-top: 50px;
    ul {
      padding: 0;
      list-style: none;
      display: flex;
      flex-direction: row;
      justify-content: center;
      li {
        @media only screen and (max-device-width: 768px) {
          margin: 0 7px;
          padding: 25px;
          font-size: 26px;
          color: white;
        }
        cursor: pointer;
        margin: 0 17px;
        padding: 30px;
        font-size: 34px;
        border: 1px solid white;
        color: white;
        border-radius: 37px;
        transition: all 0.3s;
        position: relative;
        a {
          left: 25%;
          top: 18%;
          position: absolute;
          i {
            transition: all 0.3s;
          }
        }
        &:hover {
          border: 1px solid $yellow !important;
          a {
            i {
              color: $yellow;
            }
          }
        }
      }
    }
    margin-bottom: 21px;
  }
  .copyright {
    font-size: 16px;
  }
  .other-links {
    @media only screen and (max-device-width: 768px) {
      padding-right: 10px;
      padding-top: 30px;
    }
    padding-right: 82px;
    ul {
      padding: 0px;
      margin-top: 33px;
      list-style: none;
      li {
        font-size: 16px;
        padding-top: 16px;
        transition: all 0.3s;
        color: white;
        :hover {
          color: $yellow;
        }
      }
    }
  }
}

</style>
