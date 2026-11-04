class RecoveryCodes < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :code_hash, presence: true

  belongs_to :user, class_name: "Users", inverse_of: :recovery_codes
end
