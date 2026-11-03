class MastraSchedules < ApplicationRecord
  validates :target, exclusion: { in: [ nil ] }
  validates :cron, presence: true
  validates :status, presence: true
  validates :next_fire_at, presence: true
  validates :created_at, presence: true
  validates :updated_at, presence: true
end
