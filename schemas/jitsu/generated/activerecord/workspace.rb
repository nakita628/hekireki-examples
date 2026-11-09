class Workspace < ApplicationRecord
  self.table_name = "Workspace"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :deleted, default: false
  attribute :featuresEnabled, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, uniqueness: { allow_nil: true }
  validates :slug, uniqueness: { allow_nil: true }

  has_many :workspace_access, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :restrict_with_error
  has_many :workspace_options, class_name: "WorkspaceOptions", foreign_key: "workspaceId", inverse_of: :workspace,
                               dependent: :restrict_with_error
  has_many :configuration_object, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :restrict_with_error
  has_many :configuration_object_link, foreign_key: "workspaceId", inverse_of: :workspace,
                                       dependent: :restrict_with_error
  has_many :preferences, class_name: "UserPreferences", foreign_key: "workspaceId", inverse_of: :workspace,
                         dependent: :nullify
  has_many :workspace_user_properties, class_name: "WorkspaceUserProperties", foreign_key: "workspaceId",
                                       inverse_of: :workspace, dependent: :restrict_with_error
  has_many :profile_builders, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :restrict_with_error
  has_many :oidc_login_groups, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :restrict_with_error
end
