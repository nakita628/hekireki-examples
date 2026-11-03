class ConnectedGithubRepository < ApplicationRecord
  self.table_name = "ConnectedGithubRepository"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :previewDeploymentsEnabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, uniqueness: true
  validates :projectId, uniqueness: true
  validates :branchTracking, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :connected_github_repository
  belongs_to :repository, class_name: "GithubRepository", foreign_key: "repositoryId",
                          inverse_of: :connected_github_repository
end
