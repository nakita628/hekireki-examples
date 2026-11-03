class VerificationToken < ApplicationRecord
  self.table_name = "VerificationToken"
  self.primary_key = %w[identifier token]

  attribute :expires, PrismaDateTime.new

  validates :token, uniqueness: true
  validates :expires, presence: true
end
