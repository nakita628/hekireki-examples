class OauthResource < ApplicationRecord
  self.table_name = "OauthResource"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :allowedScopes, default: -> { [] }
  attribute :dpopBoundAccessTokensRequired, default: false
  attribute :disabled, default: false
  attribute :policyVersion, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true, uniqueness: true
  validates :identifier, uniqueness: true
  validates :name, presence: true

  has_many :oauth_client_resources, foreign_key: "resourceId", primary_key: "identifier", inverse_of: :oauth_resource,
                                    dependent: :destroy
end
