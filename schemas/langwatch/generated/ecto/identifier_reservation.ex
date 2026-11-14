defmodule LangWatch.IdentifierReservation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          normalized_value: String.t(),
          user_id: String.t(),
          identifier_id: String.t(),
          command_id: String.t(),
          inserted_at: DateTime.t()
        }

  schema "IdentifierReservation" do
    field(:normalized_value, :string, primary_key: true, source: :normalizedValue)
    field(:user_id, :string, source: :userId)
    field(:identifier_id, :string, source: :identifierId)
    field(:command_id, :string, source: :commandId)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end