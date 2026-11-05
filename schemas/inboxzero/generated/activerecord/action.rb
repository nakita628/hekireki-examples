class Action < ApplicationRecord
  self.table_name = "Action"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { archive: "ARCHIVE", label: "LABEL", reply: "REPLY", send_email: "SEND_EMAIL", forward: "FORWARD", draft_email: "DRAFT_EMAIL", draft_messaging_channel: "DRAFT_MESSAGING_CHANNEL", notify_messaging_channel: "NOTIFY_MESSAGING_CHANNEL", mark_spam: "MARK_SPAM", call_webhook: "CALL_WEBHOOK", mark_read: "MARK_READ", star: "STAR", delete: "DELETE", digest: "DIGEST", move_folder: "MOVE_FOLDER", notify_sender: "NOTIFY_SENDER", integration: "INTEGRATION" },
       prefix: true, validate: true

  belongs_to :rule, foreign_key: [ :ruleId, :emailAccountId ], primary_key: [ :id, :emailAccountId ],
                    inverse_of: :actions
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :actions
  belongs_to :messaging_channel, foreign_key: [ :messagingChannelId, :messagingChannelEmailAccountId ],
                                 primary_key: [ :id, :emailAccountId ], optional: true, inverse_of: :actions
end
