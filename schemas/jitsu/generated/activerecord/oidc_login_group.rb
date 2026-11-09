class OidcLoginGroup < ApplicationRecord
  self.table_name = "OidcLoginGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :allowAllUsers, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :workspaceId, uniqueness: { scope: :oidcProviderId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :oidc_login_groups
  belongs_to :oidc_provider, foreign_key: "oidcProviderId", inverse_of: :oidc_login_groups
end
