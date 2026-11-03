class OAuthSelfHostedAuthorization < ApplicationRecord
  self.table_name = "OAuthSelfHostedAuthorization"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :mcpUrl, presence: true
  validates :apiKey, presence: true

  belongs_to :oauth_app, class_name: "OAuthApp", foreign_key: "oauthAppId", inverse_of: :self_hosted_authorizations
end
