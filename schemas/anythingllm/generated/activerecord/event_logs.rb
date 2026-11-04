class EventLogs < ApplicationRecord
  attribute :occurredAt, PrismaDateTime.new, default: -> { Time.current }

  validates :event, presence: true
end
