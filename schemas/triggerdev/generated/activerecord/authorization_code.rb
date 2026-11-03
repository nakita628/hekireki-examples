class AuthorizationCode < ApplicationRecord
  self.table_name = "AuthorizationCode"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :code, presence: true, uniqueness: true

  belongs_to :personal_access_token, foreign_key: "personalAccessTokenId", optional: true,
                                     inverse_of: :authorization_codes
end
