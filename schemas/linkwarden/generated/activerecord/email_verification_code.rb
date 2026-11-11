class EmailVerificationCode < ApplicationRecord
  self.table_name = "EmailVerificationCode"

  attribute :attempts, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true, uniqueness: true
  validates :code, presence: true
  validates :expires, presence: true
end
