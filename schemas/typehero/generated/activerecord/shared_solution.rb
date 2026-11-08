class SharedSolution < ApplicationRecord
  self.table_name = "SharedSolution"

  attribute :isPinned, default: false
  alias_attribute :created_at, :createdAt

  validates :title, presence: true
  validates :description, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :shared_solution
  belongs_to :challenge, foreign_key: "challengeId", optional: true, inverse_of: :shared_solution
  has_many :vote, foreign_key: "sharedSolutionId", inverse_of: :shared_solution, dependent: :nullify
  has_many :solution_comment, class_name: "Comment", foreign_key: "rootSolutionId", inverse_of: :root_solution,
                              dependent: :nullify
  has_many :report, foreign_key: "solutionId", inverse_of: :solution, dependent: :nullify
  has_many :notification, foreign_key: "sharedSolutionId", inverse_of: :shared_solution, dependent: :nullify
end
