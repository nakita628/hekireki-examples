defmodule Calcom.ResetPasswordRequest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          expires: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ResetPasswordRequest" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:expires, Calcom.PrismaDateTime)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end