class UserPassword < ApplicationRecord
  self.table_name = "UserPassword"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :changeAtNextLogin, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: true
  validates :hash, presence: true

  belongs_to :user, class_name: "UserProfile", foreign_key: "userId", inverse_of: :password
end
