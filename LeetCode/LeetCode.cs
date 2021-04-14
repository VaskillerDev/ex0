using System;
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
    
    public class ListNode
    {
        public ListNode next;
        public int val;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
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
        /*
         * Two trees are a mirror reflection of each other if:
         * Their two roots have the same value.
         * The right subtree of each tree is a mirror reflection of the left subtree of the other tree.
         */
        public static bool IsSymmetricTree(TreeNode node)
        {
            if (node == null) return false; // guard
        
            return IsMirror(node, node);
        }
        
        private static bool IsMirror(TreeNode nodeLeft, TreeNode nodeRight)
        {
            //    /   \ outer
            //     \ /  inner
        
            if (nodeLeft == null && nodeRight == null) return true;
            if (nodeLeft == null || nodeRight == null) return false;
        
            var isEqual = nodeLeft.val == nodeRight.val;
            var isOuterMirror = IsMirror(nodeLeft.left, nodeRight.right);
            var isInnerMirror = IsMirror(nodeLeft.right, nodeRight.left);
        
            return isEqual && isOuterMirror && isInnerMirror;
        }
        
        /*
         Given the root of a binary tree and an integer targetSum, return true if the tree 
         has a root-to-leaf path such that adding up all the values along the path equals targetSum.
         A leaf is a node with no children.
         */
        public static bool HasPathSum(TreeNode node, int targetSum, int currentSum = 0)
        {
            if (node == null) return false;
            var isLeaf = node.left == null && node.right == null;
        
            currentSum += node.val;
        
            if (currentSum == targetSum && isLeaf) return true;
        
            bool resL = false;
            if (node.left != null) resL = HasPathSum(node.left, targetSum, currentSum);
            bool resR = false;
            if (node.right != null) resR = HasPathSum(node.right, targetSum, currentSum);
        
            return resL || resR;
        }
    }
    
    public static class PrincipeOfRecursion
    {
        public static void ReverseString(char[] s)
        {
            int l = 0;
            int r = s.Length - 1;
            while (l < r)
            {
                var tmp = s[l];
                s[l] = s[r];
                s[r] = tmp;
    
                ++l;
                --r;
            }
        }
        
        public static ListNode SwapPairs(ListNode node)
         {
            if (node == null) return null;
            if (node.next == null) return node;
            
            var newNode = Swap(node, node.next);
            if (newNode.next.next != null) newNode.next.next = SwapPairs(newNode.next.next);
            return newNode;
         }
        
         private static ListNode Swap(ListNode inNode, ListNode postNode)
         {
            inNode.next = null;
            var targetNode = postNode.next;
        
            postNode.next = inNode;
            inNode.next = targetNode;
            
            return postNode; 
         }
         
         public static ListNode ReverseList(ListNode node) 
         {
            if (node?.next == null) return node;
            
            var newNode = ReverseList(node.next);
            node.next.next = node;
            node.next = null;
            
            return newNode;
         }
         
         public static TreeNode SearchBst(TreeNode node, int val)
         {
             if (node == null) return null;
             if (node.val == val) return node;

             TreeNode res = null;
             if (node.left != null) res = SearchBst(node.left, val);
             if (node.right != null && res == null) res = SearchBst(node.right, val);

             return res;
         }
         
         /*
         The Fibonacci numbers, commonly denoted F(n) form a sequence, called the Fibonacci sequence,
         such that each number is the sum of the two preceding ones, starting from 0 and 1.
         */
         public static int Fib(int n)
         {
            if (n == 0) return 0; // base case
            if (n == 1) return 1;
         
            return Fib(n - 1) + Fib(n - 2); // recurrence relation
         }
         
         // Implement pow(x, n), which calculates x raised to the power n (i.e., x^n).
         public static double Pow(double x, int n, double multiplier = 0, int side = 0)
         {
             if (multiplier == 0) multiplier = x;
             if (side == 0) side = n > 0 ? 1 : -1;
             if (n == 1) return x;

             _ = side == 1 ? --n : ++n;
             _ = side == 1 ? x *= multiplier : x /= multiplier;

             return Pow(x, n, multiplier, side);
         }
    
    }
    
    public static class BinarySearch
    {
        /*
         Given an array of integers nums which is sorted in ascending order,
         and an integer target, write a function to search target in nums.
         If target exists, then return its index. Otherwise, return -1.
         */
        public static int Search(int[] nums, int target)
        {
            var pivot = 0;
            var left = 0;
            var right = nums.Length - 1;

            while (left <= right)
            {
                pivot = left + (right - left) / 2;
                if (nums[pivot] == target) return pivot;
                if (nums[pivot] > target)
                {
                    right = pivot - 1;
                }
                else
                {
                    left = pivot + 1;
                }
            }
            return -1;
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