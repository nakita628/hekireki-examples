class ScheduledJobRuns < ApplicationRecord
  attribute :status, default: "queued"
  attribute :startedAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :completedAt, PrismaDateTime.new
  attribute :readAt, PrismaDateTime.new

  belongs_to :job, class_name: "ScheduledJobs", foreign_key: "jobId", inverse_of: :runs
end
