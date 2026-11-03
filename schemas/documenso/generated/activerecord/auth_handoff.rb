class AuthHandoff < ApplicationRecord
  self.table_name = "AuthHandoff"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt

  validates :type, presence: true
  validates :payload, presence: true
  validates :expiresAt, presence: true
end
