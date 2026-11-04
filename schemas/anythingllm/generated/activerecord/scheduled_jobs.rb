class ScheduledJobs < ApplicationRecord
  attribute :enabled, default: true
  attribute :lastRunAt, PrismaDateTime.new
  attribute :nextRunAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :updatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :prompt, presence: true
  validates :schedule, presence: true

  has_many :runs, class_name: "ScheduledJobRuns", foreign_key: "jobId", inverse_of: :job, dependent: :destroy
end
