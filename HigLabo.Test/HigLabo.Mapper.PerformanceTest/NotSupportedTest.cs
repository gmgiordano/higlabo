﻿using FastMapper;
using HigLabo.Core;
using Mapster;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileObjects;
using BenchmarkDotNet.Attributes;

namespace HigLabo.Mapper.TestNotSupported
{
    public class NotSupportedTest
    {
        public static void HigLaboMapperTest()
        {
            var config = ObjectMapConfig.Current;

            var b1 = new Building();
            var b2 = config.Map(b1, new Building());

            var tn = TreeNode.Create();
            var tn2 = config.Map(tn, new TreeNode());

            var p1 = Park.Create();
            var p2 = config.Map(p1);
        }
        public static void TinyMapperTest()
        {
            TinyMapper.Bind<Park, Park>();

            var b1 = new Building();
            var b2 = TinyMapper.Map<Building>(b1);

            var p1 = Park.Create();
            var p2 = TinyMapper.Map<Park>(p1);//p2.Cars.Count = 0 is unexpected.Collection element does not copied to property with private setter.

            return;

            //StackoverflowException thrown
            TinyMapper.Bind<TreeNode, TreeNode>();
            TinyMapper.Bind<TreeNode, TreeNodeTarget>();

            var tn = TreeNode.Create();
            var tn2 = TinyMapper.Map<TreeNode>(tn);
        }
        public static void MapsterTest()
        {
            var b1 = new Building();
            var b2 = Mapster.TypeAdapter.Adapt<Building>(b1);
            //p2.Cars is null

            return;

            //StackoverflowException thrown
            var tn = TreeNode.Create();
            var tn2 = Mapster.TypeAdapter.Adapt<TreeNodeTarget>(tn);
        }
        public static void ExpressMapperTest()
        {
            var c1 = Car.Create();
            var c2 = ExpressMapper.Mapper.Map<Car, Car>(c1);//Exception!

            return;
            //StackoverflowException thrown
            var tn = TreeNode.Create();
            var tn2 = Mapster.TypeAdapter.Adapt<TreeNodeTarget>(tn);
        }
        public static void FastMapperTest()
        {
            var c1 = Car.Create();
            var c2 = FastMapper.TypeAdapter.Adapt<Car, Car>(c1);//Exception!

            return;
            //StackoverflowException thrown
            var tn = TreeNode.Create();
            var tn2 = Mapster.TypeAdapter.Adapt<TreeNodeTarget>(tn);
        }
    }

    public class TreeNode
    {
        public String Name { get; set; } = "Node1";
        public List<TreeNode> Nodes { get; set; }

        public TreeNode()
        {
            this.Nodes = new List<TreeNode>();
        }
        public static TreeNode Create()
        {
            var tn = new TreeNode();
            tn.Nodes.Add(new TreeNode());
            tn.Nodes[0].Nodes.Add(tn);
            return tn;
        }
    }
    public class TreeNodeTarget
    {
        public String Name { get; set; } = "Node2";
        public List<TreeNodeTarget> Nodes { get; private set; }

        public TreeNodeTarget()
        {
            this.Nodes = new List<TreeNodeTarget>();
        }
    }
    public class Building
    {
        public String Name { get; set; }

        public Park Park { get; set; }

        public Building()
        {
            this.Name = "Building123";
            this.Park = Park.Create();
        }
    }

    public class Park
    {
        public List<Car> Cars { get; private set; }

        public Park()
        {
            this.Cars = new List<Car>();
        }

        public static Park Create()
        {
            var p1 = new Park();
            var c1 = new Car();
            c1.Vector = new Vector2(2, 3);
            p1.Cars.Add(c1);
            return p1;
        }
    }
    public class ParkTarget
    {
        public Car[] Cars { get; set; }

        public ParkTarget()
        {
        }
    }
    public class Car
    {
        public String Name { get; set; } = "Toyota";
        public Vector2 Vector { get; set; }

        public static Car Create()
        {
            var c1 = new Car();
            c1.Vector = new Vector2(2, 3);
            return c1;
        }
    }
    public struct Vector2
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Vector2(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}