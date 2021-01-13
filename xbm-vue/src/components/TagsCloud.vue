<template>
  <div class="tagsCloud">
   <div id="wrap">
            <a href="#" class="tag" v-for="(item,idx) in tags" :key="idx" @click="clickItem(item)">{{item}}</a>
        </div>
  </div>
</template>

<script>
// import '../../../static/js/tagsCloud.js'
export default {
  data: function() {
    return {
      tags: [
        "时间",
        "李佳",
        "信息",
        "部门",
        "法规",
        "流程",
        "申请",
        "绩效",
        "开会",
        "金额",
        "合同",
        "地点",
        "视频会议",
        "参加",
        "王云",
        "收件人",
        "发件人"
      ]
    };
  },
  mounted() {
    this.drawTags()
  },
  methods: {
    drawTags: function() {
        const _baseAngle = Math.PI / 360,
        R = 200;
      let speed = 0.1,
        angleX = speed * _baseAngle,
        angleY = -speed * _baseAngle,
        _focalLength = R * 1.5;

      function Initialization(options) {
        this.options = options;
        this.container = options.container;
        this.dataArr = options.data;
        this.init();
      }

      Initialization.prototype.init = function() {
        let len = this.dataArr.length;
        let newTags = [];

        for (let i = 0; i < len; i++) {
          var angleA = Math.acos((2 * (i + 1) - 1) / len - 1);
          var angleB = angleA * Math.sqrt(len * Math.PI);
          var z = R * Math.cos(angleA);
          var y = R * Math.sin(angleA) * Math.sin(angleB);
          var x = R * Math.sin(angleA) * Math.cos(angleB);
          var color = "#" + Math.floor(Math.random() * 0xffffff).toString(16);
          this.dataArr[i].style.color = color;
          var newtag = new Tag(this.dataArr[i], x, y, z, this.options);
          newtag.move();
          newTags.push(newtag);
          this.animate();
        }
        this.newTags = newTags;
      };

      Initialization.prototype.rotateX = function() {
        let cos = Math.cos(angleX),
          sin = Math.sin(angleX);
        this.newTags.forEach(tag => {
          let y = tag.y * cos - tag.z * sin,
            z = tag.z * cos + tag.y * sin;
          tag.y = y;
          tag.z = z;
        });
      };

      Initialization.prototype.rotateY = function() {
        let cos = Math.cos(angleY),
          sin = Math.sin(angleY);
        this.newTags.forEach(tag => {
          let x = tag.x * cos - tag.z * sin,
            z = tag.z * cos + tag.x * sin;
          tag.x = x;
          tag.z = z;
        });
      };
      Initialization.prototype.animate = function() {
        var that = this;
        setInterval(function() {
          that.rotateX();
          that.rotateY();
          that.newTags.forEach(tag => {
            tag.move();
          });
        }, 20);
      };
      function Tag(data, x, y, z, options) {
        this.options = options;
        this.dataArr = options.data;
        this.data = data;
        this.x = x;
        this.y = y;
        this.z = z;
      }
      Tag.prototype.move = function() {
        var len = this.dataArr.length;
        var scale = _focalLength / (_focalLength - this.z);
        var alpha = (this.z + R) / (2 * R);
        // console.log(this.x);
        this.data.style.left = this.x + "px";
        this.data.style.top = this.y + "px";
        this.data.style.fontSize = 14 * scale + "px";
        this.data.style.opacity = alpha + 0.5;
      };
      // window.onload = function() {
            let tags = document.getElementsByClassName("tag");
        let wrap = document.getElementById("wrap");

        let options = {
          data: tags,
          container: wrap
        };
        let tagCloud = new Initialization(options);
        document.addEventListener("mousemove", function(e) {
          angleY =
            2 *
            (e.clientX / document.body.getBoundingClientRect().width - 0.5) *
            speed *
            _baseAngle;
          angleX =
            2 *
            (e.clientY / document.body.getBoundingClientRect().height - 0.5) *
            speed *
            _baseAngle;
        });
      // }
    },
    clickItem: function(item) {
      this.$message('您点击了'+item)
    }
  }
};
</script>

<style lang="scss" scoped>
.tagsCloud {
  
  width: 100%;
  height: 100%;
  #wrap {
    width:200px;
    left: 50%;
    top: 200px;
    //  height:200px;
     position: relative;
    /*text-align: center;*/
  }
  #wrap .tag {
    display: inline-block;
    position: absolute;
  //  width: 50px;
    height: 50px;
    line-height: 1.5;
    font-size: 20px;
    text-decoration: none;
    /*left: 10px;*/
  }
}
</style>
