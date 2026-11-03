defmodule Typebot.Coupon do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_properties_to_update: map(),
          code: String.t(),
          date_redeemed: DateTime.t() | nil
        }

  schema "Coupon" do
    field(:user_properties_to_update, :map, source: :userPropertiesToUpdate)
    field(:code, :string, primary_key: true)
    field(:date_redeemed, Typebot.PrismaDateTime, source: :dateRedeemed)
  end
end