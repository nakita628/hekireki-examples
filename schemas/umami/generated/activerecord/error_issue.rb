class ErrorIssue < ApplicationRecord
  self.table_name = "error_issue"
  self.primary_key = "issue_id"

  attribute :grouping_version, default: 1
  attribute :status, default: "unresolved"

  validates :website_id, presence: true, uniqueness: { scope: %i[grouping_version fingerprint] }
  validates :fingerprint, presence: true, length: { maximum: 64 }
  validates :title, presence: true, length: { maximum: 2200 }
  validates :status, length: { maximum: 20 }
end
