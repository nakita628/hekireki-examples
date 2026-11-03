defmodule Teable.Waitlist do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          email: String.t(),
          invite: boolean() | nil,
          invite_time: DateTime.t() | nil,
          created_time: DateTime.t()
        }

  schema "waitlist" do
    field(:email, :string, primary_key: true)
    field(:invite, :boolean)
    field(:invite_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
  end
end