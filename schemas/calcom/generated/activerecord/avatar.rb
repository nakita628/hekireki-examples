class Avatar < ApplicationRecord
  attribute :teamId, default: 0
  attribute :userId, default: 0
  attribute :isBanner, default: false

  validates :teamId, uniqueness: { scope: [ :userId, :isBanner ] }
  validates :data, presence: true
  validates :objectKey, presence: true, uniqueness: true
end
