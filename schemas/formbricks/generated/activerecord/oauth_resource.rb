class OauthResource < ApplicationRecord
  self.table_name = "oauthResource"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :allowedScopes, default: -> { [] }
  attribute :dpopBoundAccessTokensRequired, default: false
  attribute :disabled, default: false
  attribute :policyVersion, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true, uniqueness: true
  validates :name, presence: true

  has_many :clients, class_name: "OauthClientResource", foreign_key: "resourceId", primary_key: "identifier",
                     inverse_of: :resource, dependent: :destroy
end
