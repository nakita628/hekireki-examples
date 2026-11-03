class Token < ApplicationRecord
  self.table_name = "Token"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :hashedKey, presence: true, uniqueness: true
  validates :partialKey, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :tokens
end
