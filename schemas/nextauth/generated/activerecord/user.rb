class User < ApplicationRecord
  self.table_name = "User"

  attribute :id, default: -> { Cuid.generate }
  attribute :emailVerified, PrismaDateTime.new

  validates :email, presence: true, uniqueness: true

  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :authenticator, foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
