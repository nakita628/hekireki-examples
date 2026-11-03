class Vote < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :type, { yes: "yes", no: "no", if_need_be: "ifNeedBe" }, default: :yes, validate: true

  belongs_to :participant, inverse_of: :votes
  belongs_to :option, inverse_of: :votes
  belongs_to :poll, inverse_of: :votes
end
