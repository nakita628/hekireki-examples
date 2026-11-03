class ApiToken < ApplicationRecord
  self.table_name = "ApiToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :name, presence: true

  belongs_to :owner, class_name: "User", foreign_key: "ownerId", inverse_of: :api_tokens
end
