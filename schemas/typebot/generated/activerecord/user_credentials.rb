class UserCredentials < ApplicationRecord
  self.table_name = "UserCredentials"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :data, presence: true
  validates :name, presence: true
  validates :type, presence: true
  validates :iv, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :credentials
end
