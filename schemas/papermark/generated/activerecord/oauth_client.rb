class OAuthClient < ApplicationRecord
  self.table_name = "OAuthClient"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :redirectUris, default: -> { [] }
  attribute :grantTypes, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :clientId, presence: true, uniqueness: true
  validates :clientName, presence: true
  validates :scopes, presence: true
  validates :tokenAuthMethod, presence: true
  validates :registrationType, presence: true

  has_many :tokens, class_name: "RestrictedToken", foreign_key: "clientId", inverse_of: :oauth_client,
                    dependent: :nullify
end
