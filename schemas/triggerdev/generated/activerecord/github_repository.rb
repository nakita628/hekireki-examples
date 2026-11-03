class GithubRepository < ApplicationRecord
  self.table_name = "GithubRepository"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :githubId, presence: true, uniqueness: { scope: :installationId }
  validates :name, presence: true
  validates :fullName, presence: true
  validates :htmlUrl, presence: true
  validates :private, inclusion: { in: [ true, false ] }
  validates :defaultBranch, presence: true

  belongs_to :installation, class_name: "GithubAppInstallation", foreign_key: "installationId",
                            inverse_of: :repositories
  has_many :connected_github_repository, foreign_key: "repositoryId", inverse_of: :repository, dependent: :destroy
end
