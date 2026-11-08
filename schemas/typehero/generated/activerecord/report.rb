class Report < ApplicationRecord
  self.table_name = "Report"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { challenge: "CHALLENGE", user: "USER", comment: "COMMENT", solution: "SOLUTION" }, validate: true
  enum :status, { pending: "PENDING", cleared: "CLEARED", dismissed: "DISMISSED" }, validate: true

  validates :id, uniqueness: true
  validates :text, presence: true

  belongs_to :reporter, class_name: "User", foreign_key: "reporterId", inverse_of: :reports
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :reports_about
  belongs_to :challenge, foreign_key: "challengeId", optional: true, inverse_of: :report
  belongs_to :comment, foreign_key: "commentId", optional: true, inverse_of: :report
  belongs_to :solution, class_name: "SharedSolution", foreign_key: "solutionId", optional: true, inverse_of: :report
  belongs_to :moderator, class_name: "User", foreign_key: "moderatorId", optional: true, inverse_of: :moderated
  has_many :issues, class_name: "ReportIssue", foreign_key: "reportId", inverse_of: :report,
                    dependent: :restrict_with_error
end
