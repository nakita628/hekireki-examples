class OAuthGrantTeam < ApplicationRecord
  self.table_name = "OAuthGrantTeam"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :grantId, presence: true, uniqueness: true
  validates :teamId, presence: true
  validates :userId, presence: true
end
