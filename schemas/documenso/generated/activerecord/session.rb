class Session < ApplicationRecord
  self.table_name = "Session"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isEmbed, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :sessionToken, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :sessions
end
