class MailSplit < ApplicationRecord
  self.table_name = "MailSplit"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :matchAll, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :emailAccountId }
  validates :order, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :mail_splits
  has_many :filters, class_name: "MailSplitFilter", foreign_key: "mailSplitId", inverse_of: :mail_split,
                     dependent: :destroy
end
