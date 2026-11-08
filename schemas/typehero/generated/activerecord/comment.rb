class Comment < ApplicationRecord
  self.table_name = "Comment"

  attribute :visible, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :rootType, { solution: "SOLUTION", challenge: "CHALLENGE" }, default: :challenge, validate: true

  validates :text, presence: true

  belongs_to :parent_comment, class_name: "Comment", foreign_key: "parentId", optional: true, inverse_of: :replies
  belongs_to :user, foreign_key: "userId", inverse_of: :comment
  belongs_to :root_challenge, class_name: "Challenge", foreign_key: "rootChallengeId", optional: true,
                              inverse_of: :comment
  belongs_to :root_solution, class_name: "SharedSolution", foreign_key: "rootSolutionId", optional: true,
                             inverse_of: :solution_comment
  has_many :replies, class_name: "Comment", foreign_key: "parentId", inverse_of: :parent_comment,
                     dependent: :restrict_with_error
  has_many :report, foreign_key: "commentId", inverse_of: :comment, dependent: :nullify
  has_many :vote, foreign_key: "commentId", inverse_of: :comment, dependent: :nullify
  has_many :notification, foreign_key: "commentId", inverse_of: :comment, dependent: :nullify
end
