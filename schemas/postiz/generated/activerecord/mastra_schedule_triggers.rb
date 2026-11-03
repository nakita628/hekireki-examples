class MastraScheduleTriggers < ApplicationRecord
  validates :schedule_id, presence: true
  validates :scheduled_fire_at, presence: true
  validates :actual_fire_at, presence: true
  validates :outcome, presence: true
  validates :trigger_kind, presence: true
end
