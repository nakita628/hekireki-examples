class GithubAppInstallation < ApplicationRecord
  self.table_name = "GithubAppInstallation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :repositorySelection, { all: "ALL", selected: "SELECTED" }, prefix: true, validate: true

  validates :appInstallationId, presence: true, uniqueness: true
  validates :targetId, presence: true
  validates :targetType, presence: true
  validates :accountHandle, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :github_app_installations
  has_many :repositories, class_name: "GithubRepository", foreign_key: "installationId", inverse_of: :installation,
                          dependent: :destroy
end
