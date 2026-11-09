class OAuthClient < ApplicationRecord
  self.table_name = "OAuthClient"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :redirectUris, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :clientSecretHash, presence: true
  validates :name, presence: true

  has_many :tokens, class_name: "UserApiToken", foreign_key: "oauthClientId", inverse_of: :oauth_client,
                    dependent: :nullify
end
