class GithubPullRequest < ApplicationRecord
  self.table_name = "GithubPullRequest"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :isDraft, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :repositoryHost, presence: true, uniqueness: { scope: [ :organizationId, :repositoryFullName, :prNumber ] }
  validates :repositoryFullName, presence: true
  validates :headBranch, presence: true
  validates :prNumber, presence: true
  validates :htmlUrl, presence: true
  validates :title, presence: true
  validates :state, presence: true
  validates :prCreatedAt, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :github_pull_requests
end
