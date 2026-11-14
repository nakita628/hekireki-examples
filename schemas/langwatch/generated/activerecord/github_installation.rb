class GithubInstallation < ApplicationRecord
  self.table_name = "GithubInstallation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :installationId, presence: true, uniqueness: true
  validates :accountLogin, presence: true
  validates :accountType, presence: true
  validates :accountId, presence: true
  validates :repositorySelection, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :github_installations
end
