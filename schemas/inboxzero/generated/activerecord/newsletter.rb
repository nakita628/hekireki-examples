class Newsletter < ApplicationRecord
  self.table_name = "Newsletter"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :patternAnalyzed, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { approved: "APPROVED", unsubscribed: "UNSUBSCRIBED", auto_archived: "AUTO_ARCHIVED" },
       validate: { allow_nil: true }

  validates :email, presence: true, uniqueness: { scope: :emailAccountId }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :newsletters
  belongs_to :category, foreign_key: "categoryId", optional: true, inverse_of: :email_senders
end
