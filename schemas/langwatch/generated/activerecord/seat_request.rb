class SeatRequest < ApplicationRecord
  self.table_name = "SeatRequest"

  attribute :idpRefused, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true, uniqueness: { scope: :email }
  validates :email, presence: true
  validates :status, presence: true
  validates :source, presence: true
  validates :openedAt, presence: true
end
