class MastraTraces < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :traceId, presence: true
  validates :scope, presence: true
  validates :kind, presence: true
  validates :startTime, presence: true
  validates :endTime, presence: true
end
