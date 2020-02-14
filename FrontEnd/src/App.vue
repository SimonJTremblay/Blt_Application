<template>
  <div id="app">
    <Header class="sticky"/>
    <router-view class="content"/>
  </div>
</template>

<script>
import Header from './components/layout/Header';
export default {
  name:"app",
  components: {
    Header
  }
}
</script>

<style>
  /* #app {} */
  *{
    box-sizing: border-box;
    margin: 0;
    padding: 0;
  }

  body{
    font-family: Arial, Helvetica, sans-serif;
    line-height: 1.4;
  }

  .btn{    
    border: none;
    background: #555;
    color: #fff;
    padding: 7px 20px;
    cursor: pointer;
  }

  .btn:hover{
    background: rgb(114, 113, 113);
  }

  .sticky{
    position: fixed;
    top: 0;
    width: 100%;
  }
  .sticky + .content {
    padding-top: 100px;
  }
  .isRed{
    background: #f03c3c;
  }  
  .isRed:hover{
    background: #db6969;
  }
  .isYellow{
    background: #f1f12d;
  }  
  .isYellow:hover{
    background: #e9e984;
  } 
  .isGreen{
    background: #29ff29;
  }
  .isGreen:hover{
    background: #97fa97;
  }
  .isGrey{
    background: #bbb9b9;
  }
  .clickable{
    cursor: pointer;
  }
</style>
