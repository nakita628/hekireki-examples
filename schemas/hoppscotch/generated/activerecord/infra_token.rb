class InfraToken < ApplicationRecord
  self.table_name = "InfraToken"

  attribute :id, default: -> { Cuid.generate }
  attribute :token, default: -> { SecureRandom.uuid }

  validates :creatorUid, presence: true
  validates :label, presence: true
  validates :token, uniqueness: true
end
