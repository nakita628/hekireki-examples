class OutOfOfficeEntry < ApplicationRecord
  self.table_name = "OutOfOfficeEntry"

  attribute :showNotePublicly, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :uuid, presence: true, uniqueness: true
  validates :start, presence: true
  validates :end, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :booking_redirects
  belongs_to :to_user, class_name: "User", foreign_key: "toUserId", optional: true, inverse_of: :booking_redirects_to
  belongs_to :reason, class_name: "OutOfOfficeReason", foreign_key: "reasonId", optional: true, inverse_of: :entries
end
