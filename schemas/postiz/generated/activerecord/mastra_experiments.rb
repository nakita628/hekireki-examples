class MastraExperiments < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :targetType, presence: true
  validates :targetId, presence: true
  validates :status, presence: true
  validates :totalItems, presence: true
  validates :succeededCount, presence: true
  validates :failedCount, presence: true
  validates :skippedCount, presence: true
end
