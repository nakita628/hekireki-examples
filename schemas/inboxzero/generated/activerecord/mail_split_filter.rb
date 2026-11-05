class MailSplitFilter < ApplicationRecord
  self.table_name = "MailSplitFilter"

  attribute :id, default: -> { Cuid.generate }

  enum :kind, { unread: "UNREAD", starred: "STARRED", label: "LABEL", category: "CATEGORY", from: "FROM", older_than: "OLDER_THAN" },
       prefix: true, validate: true

  validates :order, presence: true, uniqueness: { scope: :mailSplitId }

  belongs_to :mail_split, foreign_key: "mailSplitId", inverse_of: :filters
end
