class GithubBranchPullRequestCheck < ApplicationRecord
  self.table_name = "GithubBranchPullRequestCheck"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :prCount, default: 0
  attribute :attempts, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :repositoryHost, presence: true,
                             uniqueness: { scope: [ :organizationId, :repositoryFullName, :headBranch ] }
  validates :repositoryFullName, presence: true
  validates :headBranch, presence: true
  validates :lastCheckedAt, presence: true
  validates :lastRequestedAt, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :github_branch_pr_checks
end
