class OutOfOfficeReason < ApplicationRecord
  self.table_name = "OutOfOfficeReason"

  attribute :enabled, default: true

  validates :emoji, presence: true
  validates :reason, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :out_of_office_reasons
  has_many :entries, class_name: "OutOfOfficeEntry", foreign_key: "reasonId", inverse_of: :reason, dependent: :nullify
end
