class Authenticator < ApplicationRecord
  self.table_name = "Authenticator"
  self.primary_key = %w[userId credentialID]

  validates :credentialID, uniqueness: true
  validates :providerAccountId, presence: true
  validates :credentialPublicKey, presence: true
  validates :counter, presence: true
  validates :credentialDeviceType, presence: true
  validates :credentialBackedUp, inclusion: { in: [ true, false ] }

  belongs_to :user, foreign_key: "userId", inverse_of: :authenticator
end
