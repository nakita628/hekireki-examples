class RetentionRunItem < ApplicationRecord
  self.table_name = "RetentionRunItem"

  attribute :id, default: -> { Cuid2.call }
  attribute :count, default: 1

  enum :targetType, { survey: "survey", user: "user" }, validate: true
  enum :action, { notified: "notified", archived: "archived", deactivated: "deactivated", deleted: "deleted", skipped: "skipped" },
       validate: true
  enum :skipReason, { exempt: "exempt", last_owner: "lastOwner", other_organization: "otherOrganization", no_recipient: "noRecipient" },
       validate: { allow_nil: true }

  validates :targetId, presence: true

  belongs_to :run, class_name: "RetentionRun", foreign_key: "runId", inverse_of: :items
end
