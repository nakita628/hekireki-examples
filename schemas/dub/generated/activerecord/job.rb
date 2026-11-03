class Job < ApplicationRecord
  self.table_name = "Job"

  attribute :attempts, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :payload, exclusion: { in: [ nil ] }
end
