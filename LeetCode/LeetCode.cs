﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LeetCode
{
    public class TreeNode
    {
        public TreeNode left;
        public TreeNode right;
        public int val;

        public TreeNode(int val, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public static class Traverse
    {
        // Pre-order traversal is to visit the root first. Then traverse the left subtree. Finally, traverse the right subtree.
        public static IList<int> PreorderTraversal(TreeNode node)
        {
            var list = new List<int>();
            if (node == null) return list;
            list.Add(node.val);

            if (node.left != null) list.AddRange(PreorderTraversal(node.left));
            if (node.right != null) list.AddRange(PreorderTraversal(node.right));

            return list;
        }

        // In-order traversal is to traverse the left subtree first. Then visit the root. Finally, traverse the right subtree.
        public static IList<int> InorderTraversal(TreeNode node)
        {
            var list = new List<int>();
            if (node == null) return list;

            if (node.left != null) list.AddRange(InorderTraversal(node.left));
            list.Add(node.val);
            if (node.right != null) list.AddRange(InorderTraversal(node.right));

            return list;
        }

        // Post-order traversal is to traverse the left subtree first. Then traverse the right subtree. Finally, visit the root.
        public static IList<int> PostorderTraversal(TreeNode node)
        {
            var list = new List<int>();
            if (node == null) return list;

            if (node.left != null) list.AddRange(PostorderTraversal(node.left));
            if (node.right != null) list.AddRange(PostorderTraversal(node.right));
            list.Add(node.val);

            return list;
        }

        /*
         * Breadth-First Search is an algorithm to traverse or search in data structures like a tree or a graph.
         * The algorithm starts with a root node and visit the node itself first.
         * Then traverse its neighbors, traverse its second level neighbors, traverse its third level neighbors, so on and so forth. 
         */
        public static IList<IList<int>> LevelOrderTraverse(TreeNode node, int level = -1,
            Dictionary<int, List<int>> dict = null)
        {
            var emptyList = new List<IList<int>>();
            if (node == null) return emptyList;
            ++level;

            dict ??= new Dictionary<int, List<int>>();
            if (!dict.ContainsKey(level)) dict[level] = new List<int>();
            dict[level].Add(node.val);

            if (node.left != null) LevelOrderTraverse(node.left, level, dict);
            if (node.right != null) LevelOrderTraverse(node.right, level, dict);

            if (level == 0)
                return dict.Select(kv => kv.Value)
                    .Cast<IList<int>>().ToList();

            return emptyList;
        }
    }

    public static class SolveProblemsRecursively
    {
        public static int MaxDepth(TreeNode node, int depth = 0, int answer = 0)
        {
            if (node == null) return answer; // guard
            ++depth;

            if (node.left == null && node.right == null) answer = depth;
            if (node.left != null) answer = Math.Max(answer, MaxDepth(node.left, depth, answer));
            if (node.right != null) answer = Math.Max(answer, MaxDepth(node.right, depth, answer));

            return answer;
        }
    }
    
    public class Test
    {
        [Test]
        public void Run()
        {
        }
    }
}